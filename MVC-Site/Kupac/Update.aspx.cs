using AdventureWorksOBPRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MVC_Site.Kupac
{
    public partial class Update : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var kupacOld = (AdventureWorksOBPRepo.Kupac)Session["update-kupac"];
            this.PreRender += Update_PreRender;
            if (IsPostBack)
            {
                try
                {
                    var id = kupacOld.IDKupac;
                    var ime = tbIme.Text.Trim();
                    var prezime = tbPrezime.Text.Trim();
                    RegularExpressionValidatorEmail.Validate();
                    var email = RegularExpressionValidatorEmail.IsValid ? tbEmail.Text.Trim() : null;
                    RegularExpressionValidatorPhone.Validate();
                    var telefon = RegularExpressionValidatorPhone.IsValid ? tbTelefon.Text.Trim() : null;
                    var grad = int.Parse(Request.Form.Get("GradID") ?? "0");

                    var Kupac = new AdventureWorksOBPRepo.Kupac
                    {
                        IDKupac = id,
                        Ime = ime,
                        Prezime = prezime,
                        Email = email,
                        Telefon = telefon,
                        Grad = Models.RepoSingleton.GetInstance().GetGrad(grad)
                    };

                    Models.RepoSingleton.GetInstance().UpdateKupac(Kupac);
                }
                catch
                {
                    ShowError();
                }
                try
                {
                    var search = new { @grad = 0, @page = 0, @take = 0 };
                    search = Cast(search, Session["kupci-search"]);
                    Response.Redirect($"Index?&take={search.take}&page={search.page}");
                }
                catch
                {
                    Response.Redirect("Index");
                }
            }
            else
            {
                PreFillTables(kupacOld);
            }
        }

        private void ShowError()
        {
            lbError.Visible = true;
            lbError.Text = "Validation not passed";
        }

        private void PreFillTables(AdventureWorksOBPRepo.Kupac kupac)
        {
            labelID.Text = kupac.IDKupac.ToString();
            tbIme.Text = kupac.Ime;
            tbPrezime.Text = kupac.Prezime;
            tbEmail.Text = kupac.Email;
            tbTelefon.Text = kupac.Telefon;
            gradselect.Attributes["GradID"] = kupac.Grad.IDGrad.ToString();
        }

        private static T Cast<T>(T typeHolder, Object x)
        {
            // typeHolder above is just for compiler magic
            // to infer the type to cast x to
            return (T)x;
        }

        private void Update_PreRender(object sender, EventArgs e)
        {
            footer.InnerHtml = $"&copy;{DateTime.Now.Year} - Luka Čabraja - 2RP2";
        }
    }
}