let unassignedHours = document.querySelectorAll(".unassigned-hours");

unassignedHours.forEach(ColorHours);

function ColorHours(item) {
    if (item.innerHTML == "0 h") {
        item.style.color = "red";
    }
    else {
        item.style.color = "green";
    }
}