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

// get gradovi json
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

// get single grad json
async function ajaxGrad(IDGrad) {
    gradresponse = await fetch("Grad/" + IDGrad);
    if (gradresponse.ok) {
        let gradjson = await gradresponse.json();
        return gradjson
    } else {
        graddropdown.innerHTML = "<option>Connection Error</option>";
        return null;
    }
}

// set dropdown values
function setStartupValues(singlegradjson) {
    drzavedropdown.value = singlegradjson.Drzava.IDDrzava;
    graddropdown.value = singlegradjson.IDGrad;
}

// get gradovi on startup
async function getGradovi(grad) {
    await ajaxGradovi(grad.Drzava.IDDrzava).then(fillGrad)
    setStartupValues(grad);
}

// fill drzave dropdown
function fillDrzave(drzavejson) {
    options = '<option value="-1">Please select a country...</option>';
    drzavejson.forEach(x => options += `<option value=${x.IDDrzava}>${x.Naziv}</option>`)
    drzavedropdown.innerHTML = options;
}

// get drzave and fill dropdown
ajaxDrzave().then(fillDrzave).then(() => {
    grad = parseInt(document.getElementById("gradselect").attributes.gradid.value);
    console.log(document.getElementById("gradselect"));
    console.log(grad);
    if (!isNaN(grad)) {
        ajaxGrad(grad).then(getGradovi)
    }
});

// fill grad on drzava change
drzavedropdown.onchange = function () {
    if (this.value > 0) {
        ajaxGradovi(this.value).then(fillGrad)
    }
}

// fill grad dropdown
function fillGrad(gradjson) {
    options = '<option value="-1">Please select a city...</option>';
    gradjson.forEach(x => options += `<option value=${x.IDGrad}>${x.Naziv}</option>`)
    graddropdown.innerHTML = options;
}