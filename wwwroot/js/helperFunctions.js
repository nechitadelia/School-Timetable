function checkName(name) {
    name = name.replace(/ /g, "");
    if (name == "") {
        return false;
    }
    else if (name.length <= 1) {
        return false;
    }
    else if (!(/^[a-zA-Z]+$/.test(name))) {
        return false;
    }
    else {
        return true;
    }
}

function checkMaxHours(hours) {
    let hoursNumber = Number(hours);
    if (hoursNumber <= 0 || hoursNumber > 20 || typeof(hoursNumber) != "number" || !Number.isInteger(hoursNumber)) {
        return false;
    }
    else {
        return true;
    }
}