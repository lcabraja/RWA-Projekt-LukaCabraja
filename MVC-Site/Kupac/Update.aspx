<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Update.aspx.cs" Inherits="MVC_Site.Kupac.Update" UnobtrusiveValidationMode="None" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Edit a customer</title>
    <link href="../Content/bootstrap.css" rel="stylesheet" />
</head>
<body>
    <div class="navbar navbar-inverse navbar-fixed-top">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
            </div>
            <div class="navbar-collapse collapse">
                <ul class="nav navbar-nav">
                    <li><a href="Index">Kupci</a></li>
                    <li><a href="../Kategorija/Index">Kategorije</a></li>
                    <li><a href="../Potkategorija/Index">Potkategorije</a></li>
                    <li><a href="../Proizvod/Index">Proizvodi</a></li>
                </ul>
            </div>
        </div>
    </div>
    <div class="container body-content">
        <form id="form1" runat="server">
            <div class="form-horizontal">
                <h4>Kupac</h4>
                <hr />
                <asp:ValidationSummary ID="validationSummary" runat="server" />
                <asp:Label ID="lbError" runat="server" ForeColor="Red" Visible="false" />
                <div class="form-group">
                    <label class="control-label col-md-2">Ime</label>
                    <div class="col-md-10">
                        <asp:Label runat="server" ID="labelID" CssClass="form-control" />
                    </div>
                </div>


                <div class="form-group">
                    <label class="control-label col-md-2">Ime</label>
                    <div class="col-md-10">
                        <asp:TextBox runat="server" ID="tbIme" CssClass="form-control" />
                        <asp:RequiredFieldValidator
                            ID="RequiredFieldValidatorIme"
                            runat="server"
                            ControlToValidate="tbIme"
                            Display="None"
                            ForeColor="Red"
                            ErrorMessage="Name was not entered." />
                    </div>
                </div>

                <div class="form-group">
                    <label class="control-label col-md-2">Prezime</label>
                    <div class="col-md-10">
                        <asp:TextBox runat="server" ID="tbPrezime" CssClass="form-control" />
                        <asp:RequiredFieldValidator
                            ID="RequiredFieldValidatorPrezime"
                            runat="server"
                            Display="None"
                            ControlToValidate="tbPrezime"
                            ForeColor="Red"
                            ErrorMessage="Surname was not entered." />
                    </div>
                </div>

                <div class="form-group">
                    <label class="control-label col-md-2">Email</label>

                    <div class="col-md-10">
                        <asp:TextBox runat="server" ID="tbEmail" CssClass="form-control" />
                         <asp:RegularExpressionValidator
                                ID="RegularExpressionValidatorEmail"
                                runat="server"
                                ControlToValidate="tbEmail"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                EnableClientScript="true"
                                Display="None"
                                ForeColor="Red"
                                ErrorMessage="Wrong Email format" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="control-label col-md-2">Telefon</label>

                    <div class="col-md-10">
                        <asp:TextBox runat="server" ID="tbTelefon" CssClass="form-control" />
                        <asp:RegularExpressionValidator
                                ID="RegularExpressionValidatorPhone"
                                runat="server"
                                ControlToValidate="tbTelefon"
                                ValidationExpression="[0-9]? ?(\([0-9]{2}\))? ?[0-9]{2,4}[ -]?[0-9]{3,4}[ -]?[0-9]{3,4}"
                                EnableClientScript="true"
                                Display="None"
                                ForeColor="Red"
                                ErrorMessage="Wrong phone number format" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="control-label col-md-2">Drzava</label>

                    <div class="col-md-10">
                        <select id="drzave" class="form-control">
                            <option value="-1">Please select a country...</option>
                        </select>
                    </div>
                </div>

                <div class="form-group">
                    <label class="control-label col-md-2">Grad</label>

                    <div class="col-md-10">
                        <select id="gradovi" class="form-control" name="GradID">
                            <option value="-1">Please select a country...</option>
                        </select>
                    </div>
                </div>

                <div class="form-group">
                    <div class="col-md-offset-2 col-md-10">
                        <input type="submit" value="Save" class="btn btn-default" />
                    </div>
                </div>
            </div>
        </form>
        <hr />
        <footer>
            <p id="footer" runat="server"></p>
        </footer>
    </div>
    <i id="gradselect" runat="server"></i>
    <script src="../Scripts/Kupac/Update.js"></script>
</body>
</html>
