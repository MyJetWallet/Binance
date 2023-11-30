﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Binance.WebSocket;
using Xunit;

namespace Binance.Tests.WebSocket
{
    [Collection("Timing Sensitive Tests")]
    public class WebSocketStreamTest
    {
        private readonly Uri _uri;
        private readonly string _subject;
        // ReSharper disable once InconsistentNaming
        private const string _message = "{}";
        private readonly WebSocketStream _stream;

        public WebSocketStreamTest()
        {
            _uri = new Uri(BinanceWebSocketStream.BaseUri);
            _subject = _uri.AbsoluteUri;
            _stream = new WebSocketStream(DefaultWebSocketClientTest.CreateMockWebSocketClient(_message));
        }

        [Fact]
        public void Properties()
        {
            ClassicAssert.NotNull(_stream.WebSocket);
            ClassicAssert.Null(_stream.Uri);

            _stream.Uri = _uri;
            _stream.Uri = _uri; // can set to same URI.

            ClassicAssert.Equal(_uri, _stream.Uri);

            _stream.Uri = null; // can set to null.

            ClassicAssert.Null(_stream.Uri);
        }

        [Fact]
        public async Task IsStreaming()
        {
            _stream.Uri = _uri;

            using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(1)))
            {
                ClassicAssert.False(_stream.IsStreaming);

                var task = _stream.StreamAsync(cts.Token);

                ClassicAssert.True(_stream.IsStreaming);

                await task;

                ClassicAssert.False(_stream.IsStreaming);
            }
        }

        [Fact]
        public async Task StreamThrows()
        {
            using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(1)))
            {
                ClassicAssert.Null(_stream.Uri);

                await ClassicAssert.ThrowsAsync<InvalidOperationException>(() => _stream.StreamAsync(cts.Token));

                _stream.Uri = _uri;

                ClassicAssert.NotNull(_stream.Uri);

                var task = _stream.StreamAsync(cts.Token);

                await ClassicAssert.ThrowsAsync<InvalidOperationException>(() => _stream.StreamAsync(cts.Token));

                await task;
            }
        }

        [Fact]
        public async Task MessageEvent()
        {
            _stream.Uri = _uri;

            var isMessageEventReceived = false;

            using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(1)))
            {
                _stream.Message += (s, e) =>
                {
                    isMessageEventReceived = e.Subject == _subject && e.Json == _message;
                };

                ClassicAssert.False(isMessageEventReceived);

                await _stream.StreamAsync(cts.Token);

                ClassicAssert.True(isMessageEventReceived);
            }
        }

        [Fact]
        public async Task WainUntilOpen()
        {
            _stream.Uri = _uri;

            using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(1)))
            {
                ClassicAssert.False(_stream.WebSocket.IsOpen);

                var task = _stream.StreamAsync(cts.Token);

                ClassicAssert.False(_stream.WebSocket.IsOpen);

                // Wait when web socket is not open.
                await _stream.WaitUntilWebSocketOpenAsync(cts.Token);

                ClassicAssert.True(_stream.WebSocket.IsOpen);

                // Wait when web socket is open.
                await _stream.WaitUntilWebSocketOpenAsync(cts.Token);

                ClassicAssert.False(cts.IsCancellationRequested);

                await task;

                ClassicAssert.True(cts.IsCancellationRequested);
            }
        }
    }
}
