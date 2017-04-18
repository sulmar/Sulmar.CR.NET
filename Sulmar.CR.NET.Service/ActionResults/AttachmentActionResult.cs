using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Sulmar.CR.NET.Service
{

    public class AttachmentActionResult : IHttpActionResult
    {
        private readonly string fileName;
        private readonly Stream stream;
        private readonly string mimeType;

        public AttachmentActionResult(string fileName, Stream stream, string mimeType)
        {
            this.fileName = fileName;
            this.stream = stream;
            this.mimeType = mimeType;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(stream);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(mimeType);

            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                CreationDate = DateTime.Now,
                FileName = fileName,
            };

            return Task.FromResult(response);
        }
    }
}
