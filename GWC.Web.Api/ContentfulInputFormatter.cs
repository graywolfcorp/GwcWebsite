using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWC.Web.Api
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ContentfulInputFormatter : TextInputFormatter

    {

        public ContentfulInputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/vnd.contentful.delivery.v1+json"));
            //SupportedEncodings.Add(Encoding.UTF8);
            //SupportedEncodings.Add(Encoding.Unicode);
        }

        public override bool CanRead(InputFormatterContext context)
        {
            return base.CanRead(context);
        }

        public override Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
        {
            throw new NotImplementedException();
        }

        protected override bool CanReadType(Type type)
        {
            return base.CanReadType(type);
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
