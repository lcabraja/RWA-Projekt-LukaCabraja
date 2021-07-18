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
console.log({ index });
indexint = !isNaN(index) ? index : 0;
console.log({ indexint });
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