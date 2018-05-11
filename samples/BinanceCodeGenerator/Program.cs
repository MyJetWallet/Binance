﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Binance;

namespace BinanceCodeGenerator
{
    internal class Program
    {
        private static async Task Main()
        {
            var assets = new List<Asset>();

            var api = new BinanceApi();

            // Get the current timestamp.
            var timestamp = await api.GetTimestampAsync();

            // Get latest currency pairs (symbols).
            var symbols = (await api.GetSymbolsAsync()).ToList();

            // Get assets.
            foreach (var symbol in symbols)
            {
                if (!assets.Contains(symbol.BaseAsset))
                    assets.Add(symbol.BaseAsset);

                if (!assets.Contains(symbol.QuoteAsset))
                    assets.Add(symbol.QuoteAsset);
            }

            // Read the symbol template file.
            var lines = (await File.ReadAllLinesAsync("Symbol.template.cs")).ToList();

            // Replace remarks.
            var index = lines.FindIndex(l => l.Contains("<remarks></remarks>"));
            lines[index] = $"    /// <remarks>File generated by {nameof(BinanceCodeGenerator)} tool.</remarks>";

            // Replace timestamp.
            index = lines.FindIndex(l => l.Contains("<<insert timestamp>>"));
            lines[index] = $"        public static readonly long LastUpdateAt = {timestamp};";

            index = lines.FindIndex(l => l.Contains("<<insert symbols>>"));
            lines.RemoveAt(index);

            // Sort symbols.
            symbols.Sort();

            var groups = symbols.GroupBy(s => s.QuoteAsset);

            // Insert definition for each currency pair (symbol).
            foreach (var group in groups)
            {
                lines.Insert(index++, $"        // {group.First().QuoteAsset}");

                foreach (var symbol in group)
                {
                    lines.Insert(index++, $"        public static Symbol {symbol.BaseAsset}_{symbol.QuoteAsset} => Cache.Get(\"{symbol.BaseAsset}{symbol.QuoteAsset}\");");
                }

                lines.Insert(index++, string.Empty);
            }
            lines.RemoveAt(index);

            index = lines.FindIndex(l => l.Contains("<<insert symbol definitions>>"));
            lines.RemoveAt(index);

            foreach(var symbol in symbols)
            {
                var orderTypes = string.Join(",", symbol.OrderTypes.Select(_ => "OrderType." + _));
                lines.Insert(index++, $"                        new Symbol(SymbolStatus.{symbol.Status}, Asset.{symbol.BaseAsset}, Asset.{symbol.QuoteAsset}, ({symbol.Quantity.Minimum}m, {symbol.Quantity.Maximum}m, {symbol.Quantity.Increment}m), ({symbol.Price.Minimum}m, {symbol.Price.Maximum}m, {symbol.Price.Increment}m), {symbol.NotionalMinimumValue}m, {symbol.IsIcebergAllowed.ToString().ToLowerInvariant()}, new List<OrderType> {{{orderTypes}}}),");
            }

            // Save the generated source code (replacing original).
            await File.WriteAllLinesAsync("../../../../../src/Binance/Symbol.cs", lines);

            // Read the asset template file.
            lines = (await File.ReadAllLinesAsync("Asset.template.cs")).ToList();

            // Replace remarks.
            index = lines.FindIndex(l => l.Contains("<remarks></remarks>"));
            lines[index] = $"    /// <remarks>File generated by {nameof(BinanceCodeGenerator)} tool.</remarks>";

            // Replace timestamp.
            index = lines.FindIndex(l => l.Contains("<<insert timestamp>>"));
            lines[index] = $"        public static readonly long LastUpdateAt = {timestamp};";

            index = lines.FindIndex(l => l.Contains("<<insert assets>>"));
            lines.RemoveAt(index);

            // Sort assets.
            assets.Sort();

            // Insert definition for each asset.
            foreach (var asset in assets)
            {
                lines.Insert(index++, $"        public static Asset {asset} => Cache.Get(\"{asset}\");");
            }

            index = lines.FindIndex(l => l.Contains("<<insert asset definitions>>"));
            lines.RemoveAt(index);

            foreach (var asset in assets)
            {
                lines.Insert(index++, $"                        new Asset(\"{asset}\", {asset.Precision}),");
            }

            // Save the generated source code (replacing original).
            await File.WriteAllLinesAsync("../../../../../src/Binance/Asset.cs", lines);

            Console.WriteLine();
            Console.WriteLine("  ...press any key to close window.");
            Console.ReadKey(true);
        }
    }
}
