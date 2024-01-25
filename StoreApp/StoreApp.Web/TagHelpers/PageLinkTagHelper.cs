using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using StoreApp.Web.Models;

namespace StoreApp.Web.TagHelpers;

/*oluşturduğumuz TagHelper'in hangi html elementini hedef aldığını ve elementin hangi
attribute özelliklerini kullandığını tanımlayalım*/
[HtmlTargetElement("div", Attributes = "page-model")]
public class PageLinkTagHelper : TagHelper
{
    /*IUrlHelper örnekleri oluşturmak için bir fabrika.*/
    private IUrlHelperFactory _urlHelperFactory;
    public PageLinkTagHelper(IUrlHelperFactory urlHelperFactory)
    {
        _urlHelperFactory = urlHelperFactory;
    }

    [ViewContext]/*bu etiketi belirtmeliyiz*/
    public ViewContext? ViewContext { get; set; } /*görünümün yürütülmesine yönelik bağlam*/
    public PageInfo? PageModel { get; set; }/*veritabanındaki ürün bilgilerini
    tanımlamak için kullanılır*/

    public string? PageAction { get; set; }/*sayfa adres bilgisini tutmak için kullanılır*/

    /*BootStrap sınıf bilgilerini aşağıdaki değişkenler ile taşırız*/
    public string PageClass { get; set; } = string.Empty;
    public string PageClassLink { get; set; } = string.Empty;
    public string PageClassActive { get; set; } = string.Empty;

    /* TagHelper'ı verilen bağlam ve çıktıyla eşzamanlı olarak çalıştırır. */
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (ViewContext != null && PageModel != null)
        {
            /*Bir uygulama içinde ASP.NET MVC için URL'ler oluşturmak üzere
            helper'a yönelik sözleşmeyi tanımlar.*/
            IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder div = new TagBuilder("div");
            for (int i = 1; i <= PageModel.TotalPages; i++)
            {
                TagBuilder link = new TagBuilder("a");/* a etiketi oluştur*/
                /* a etiketinin bağlantı bilgilerini oluştur */
                link.Attributes["href"] = urlHelper.Action(PageAction, new { page = i });

                /*BootStrap kütüphanelerini dinamik olarak ekleyelim*/
                link.AddCssClass(PageClass); /* btn sınıfı tüm değişkenlere uygulanacak */
                /*eğer sayfa seçili ise PageClassActive stilini, değilse PageClassLink stilini uygula*/
                link.AddCssClass(i == PageModel.CurrentPage ? PageClassActive : PageClassLink);

                link.InnerHtml.Append(i.ToString()); /* a etiketinin içine sayfa no yazdır */
                div.InnerHtml.AppendHtml(link);/* div içerisine html formatında a nesnesini ekle */
            }
            /*TagHelper nesnesini yazdığımız konuma işlemden sonra ne yazdırılacağı bilgisini
            output değişkeni taşır*/
            /*output değişkeninin içeriğine hazırlanan div etiketini içeriği ile birlikte
            html formatında yazdır.*/
            output.Content.AppendHtml(div.InnerHtml);
        }
    }
}