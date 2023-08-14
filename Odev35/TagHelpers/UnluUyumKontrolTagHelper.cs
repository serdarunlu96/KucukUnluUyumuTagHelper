using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Globalization;

namespace YourProjectNamespace.TagHelpers
{
    [HtmlTargetElement("div", Attributes = "kelime")]
    public class UnluUyumKontrolTagHelper : TagHelper
    {
        [HtmlAttributeName("kelime")]
        public string Kelime { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            bool unluUyumuVarMi = KelimeUnluUyumuKontrolu(Kelime);
            string mesaj = unluUyumuVarMi ? "Küçük ünlü uyumuna uyar." : "Küçük ünlü uyumuna uymaz.";
            string renk = unluUyumuVarMi ? "green" : "red";

            output.Content.SetContent(mesaj);
            output.Attributes.SetAttribute("style", $"color: {renk};");
        }

        private bool KelimeUnluUyumuKontrolu(string kelime)
        {
            if (string.IsNullOrEmpty(kelime))
            {
                return true;
            }

            char sonUnlu = SonUnluGetir(kelime);

            if ("aeıioöuü".Contains(sonUnlu))
            {
                return true;
            }
            else if ("ou".Contains(sonUnlu))
            {
                return "ae".Contains(kelime.ToLower()[0]);
            }
            else if ("aı".Contains(sonUnlu))
            {
                return "aeı".Contains(kelime.ToLower()[0]);
            }

            return false;
        }

        private char SonUnluGetir(string kelime)
        {
            CultureInfo trCulture = new CultureInfo("tr-TR");
            TextInfo textInfo = trCulture.TextInfo;
            kelime = textInfo.ToLower(kelime);

            for (int i = kelime.Length - 1; i >= 0; i--)
            {
                if ("aeıioöuü".Contains(kelime[i]))
                {
                    return kelime[i];
                }
            }

            return ' ';
        }
    }
}
