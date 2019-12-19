using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Http;
using System.Text;
using CaseStudy.Models;
using Newtonsoft.Json;
using CaseStudy.Utils;

namespace Casestudy.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("catalogue", Attributes = BrandIdAttribute)]
    public class BrandsHelper : TagHelper
    {
        private const string BrandIdAttribute = "brand";
        [HtmlAttributeName(BrandIdAttribute)]
        public string BrandId { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public BrandsHelper(IHttpContextAccessor httpContextAccessor) { _httpContextAccessor = httpContextAccessor; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (_session.Get<ProductViewModel[]>("product") != null && Convert.ToInt32(BrandId) > 0)
            {
                var innerHtml = new StringBuilder();
                ProductViewModel[] product = _session.Get<ProductViewModel[]>("product");
                innerHtml.Append("<div class=\"col-xs-12\" style=\"font-size:x-large;\"><span>Catalogue</span></div>");
                foreach (ProductViewModel item in product)
                {
                    if (item.BrandId == Convert.ToInt32(BrandId))
                    { // remove double apostrophe 
                        item.Description = item.Description.Contains("''") ? item.Description.Replace("''", "") : item.Description;
                        item.JsonData = JsonConvert.SerializeObject(item);
                        innerHtml.Append("<div class=\"col-sm-3 col-xs-12 text-center\" style=\"border:solid;\">");
                        innerHtml.Append("<span class=\"col-xs-12\"><img src=\"/images/" + item.GraphicName + "\" style=\"height: 120px; width: 120px; \"/></span>");
                        innerHtml.Append("<p><span style=\"font-size:large;\">" + item.Description.Substring(0, 10) + "...</span></p><div>");
                        innerHtml.Append("<span>More Info.<br />Click Details</span></div>");
                        innerHtml.Append("<div style=\"padding-bottom: 10px;\"><a href=\"#details_popup\" data-toggle=\"modal\" class=\"btn btn-default\"");
                        innerHtml.Append(" id=\"modalbtn" + item.Id + "\" data-id=\"" + item.Id + "\" data-details='" + item.JsonData + "'>Details</a></div></div>");
                    }
                }
                output.Content.SetHtmlContent(innerHtml.ToString());
            }
        }

    }

}

