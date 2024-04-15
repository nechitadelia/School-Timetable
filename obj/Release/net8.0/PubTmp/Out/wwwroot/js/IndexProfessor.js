//styling the unassigned hours depending on the value
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

//searchbar for professors
let allProfessors = document.querySelectorAll(".professor-entity");
let searchInput = document.querySelector("[data-search]");
let noProfessors = document.querySelector(".noProfessorsFound");

searchInput.addEventListener("input", (e) => {
    const value = e.target.value.toLowerCase();
    let checkResults = false;

    allProfessors.forEach(professor => {
        let lastName = professor.children[0].innerHTML.toLowerCase();
        let firstName = professor.children[1].innerHTML.toLowerCase();
        let name1 = lastName + " " + firstName;
        let name2 = firstName + " " + lastName;

        const isVisible = name1.includes(value) || name2.includes(value);
        professor.classList.toggle("hide", !isVisible);
    });

    //display "no results" if there are no professors found
    for (let i = 0; i < allProfessors.length; i++) {
        if (!allProfessors[i].classList.contains("hide")) {
            checkResults = true;
            i = allProfessors.length - 1;
        }
        else {
            checkResults = false;
        }
    }

    if (checkResults) {
        noProfessors.classList.add("hide");
    }
    else {
        noProfessors.classList.remove("hide");
    }
});
