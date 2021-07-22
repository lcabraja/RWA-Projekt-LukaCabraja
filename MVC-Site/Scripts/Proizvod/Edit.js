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

kategorijadropdown = document.getElementById("Kategorija");
potkategorijadropdown = document.getElementById("Potkategorija");

// AJAX fill selects
async function ajaxPotkategorije(idkat) {
    potkategorijaresponse = await fetch("../Potkategorije/" + idkat);
    if (potkategorijaresponse.ok) {
        let potkategorijejson = await potkategorijaresponse.json();
        return potkategorijejson;
    } else {
        potkategorijadropdown.innerHTML = "<option>Connection Error</option>";
        return null;
    }
}

// fill potkategorija select
function potkategorije(potkategorijejson) {
    options = '<option value="0">/</option>';
    potkategorijejson.forEach(x => options += `<option value=${x.IDPotkategorija}>${x.Naziv}</option>`)
    potkategorijadropdown.innerHTML = options;
}

// on kategorija change update potkategorija select
kategorijadropdown.onchange = function () {
    if (this.value > 0) {
        ajaxPotkategorije(this.value).then(potkategorije)
    }
}