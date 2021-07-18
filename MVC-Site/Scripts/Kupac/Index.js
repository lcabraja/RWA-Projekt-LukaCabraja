const urlParams = new URLSearchParams(window.location.search);
document.getElementById("order").selectedIndex = parseInt(urlParams.get('orderby'));

document.getElementById("order").onchange = function () {
    document.location.replace("/Kupac/Index?orderby=" + this.value);
}

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

document.getElementById("pagedown").onclick = function () {
    console.log("beep");
    // determine if there are any previous pages
    take = parseInt(getQueryStringParameterByName("take"));
    takeint = !isNaN(take) ? take : 15;
    if (parseInt(document.getElementById("anchor").attributes.remaining) < takeint) {
        console.log("you shall not pass");
        return;
    }

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

document.getElementById("pageup").onclick = function () {
    console.log("boop");
    // check if there are any remaining pages
    take = parseInt(getQueryStringParameterByName("take"));
    takeint = !isNaN(take) ? take : 15;
    if (parseInt(document.getElementById("anchor").remaining) < takeint) {
        console.log("you shall not pass");
        return;
    }

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