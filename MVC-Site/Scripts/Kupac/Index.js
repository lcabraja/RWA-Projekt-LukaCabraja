function getQueryStringParameterByName(name) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

function updateUrlParameter(url, param, value) {
    var regex = new RegExp('(' + param + '=)[^\&]+');
    return url.replace(regex, '$1' + value);
}

//check if there are any previous pages
page = parseInt(getQueryStringParameterByName("page"));
pageint = !isNaN(page) ? page : 0;
if (pageint == 0) {
    console.log("you shall not pass");
    document.getElementById("pagedown").disabled = true;
}

document.getElementById("pagedown").onclick = function () {
    console.log("beep");
    // determing next page number
    page = getQueryStringParameterByName("page");
    if (page != "") {
        pageint = parseInt(page) - 1;
        if (!isNaN(pageint)) {
            if (pageint >= 0) {
                location.replace(updateUrlParameter(location.href, "page", pageint));
            } else {
                location.replace(updateUrlParameter(location.href, "page", 0));
            }
        } else {
            location.replace(updateUrlParameter(location.href, "page", 0));
        }
    } else {
        newlocation = (location.href.includes("?") ? location.href + "&" : "?") + "page=0";
        location.replace(newlocation);
    }
}

// check if there are any remaining pages
take = parseInt(getQueryStringParameterByName("take"));
takeint = !isNaN(take) ? take : 15;
if (parseInt(document.getElementById("anchor").attributes.remaining.value) < takeint) {
    console.log("you shall not pass");
    document.getElementById("pageup").disabled = true;
}

document.getElementById("pageup").onclick = function () {
    console.log("boop");
    // determing next page number
    page = getQueryStringParameterByName("page");
    if (page != "") {
        pageint = parseInt(page) + 1;
        if (!isNaN(pageint)) {
            location.replace(updateUrlParameter(location.href, "page", pageint));
        } else {
            location.replace(updateUrlParameter(location.href, "page", 0));
        }
    } else {
        newlocation = (location.href.includes("?") ? location.href + "&" : "?") + "page=1";
        location.replace(newlocation);
    }
}

// set order in ui
index = parseInt(getQueryStringParameterByName("orderby"));
indexint = !isNaN(index) ? index : 0;
document.getElementById("order").selectedIndex = indexint;
// set order in querystring
document.getElementById("order").onchange = function () {
    orderby = getQueryStringParameterByName("orderby");
    if (orderby != "") {
        location.replace(updateUrlParameter(location.href, "orderby", this.value));
    } else {
        newlocation = (location.href.includes("?") ? location.href + "&" : "?") + "orderby=1";
        location.replace(newlocation);
    }
}

drzavedropdown = document.getElementById("drzave");
graddropdown = document.getElementById("gradovi");

// AJAX fill selects
async function ajaxDrzave() {
    drzaveresponse = await fetch("Drzave");
    if (drzaveresponse.ok) {
        let drzavejson = await drzaveresponse.json();
        return drzavejson
    } else {
        drzavedropdown.innerHTML = "<option>Connection Error</option>";
        return null;
    }
}

async function ajaxGradovi(IDDrzava) {
    gradresponse = await fetch("GradDrzava/" + IDDrzava);
    if (gradresponse.ok) {
        let gradjson = await gradresponse.json();
        return gradjson
    } else {
        graddropdown.innerHTML = "<option>Connection Error</option>";
        return null;
    }
}

// fill drzave dropdown
function fillDrzave(drzavejson) {
    options = '<option value="-1">Please select a country...</option>';
    drzavejson.forEach(x => options += `<option value=${x.IDDrzava}>${x.Naziv}</option>`)
    drzavedropdown.innerHTML = options;
}

// get drzave and fill dropdown
ajaxDrzave().then(fillDrzave);

// fill grad on drzava change
drzavedropdown.onchange = function () {
    ajaxGradovi(this.value).then(fillGrad)
}

// fill grad dropdown
function fillGrad(gradjson) {
    options = ''
    gradjson.forEach(x => options += `<option value=${x.IDGrad}>${x.Naziv}</option>`)
    graddropdown.innerHTML = options;
}

// send request on grad select
graddropdown.onchange = function () {
    console.log(this.value)
}