const form = document.getElementById("createProfessorform");
const submit = document.getElementById("createProfessorButton");
let firstName = document.getElementById("firstName");
let lastName = document.getElementById("lastName");
let maxHours = document.getElementById("Hours");

//showing an alert message when the form is submitted
submit.addEventListener("click", () => {
    let firstNameValue = firstName.value;
    let lastNameValue = lastName.value;
    let maxHoursValue = maxHours.value;

    if (checkName(firstNameValue) && checkName(lastNameValue) && checkMaxHours(maxHoursValue)) {
        setTimeout(() => {
            form.submit();
        }, 1700);

        Swal.fire({
            title: "Good job!",
            text: "Success! Professor " + firstNameValue + " " + lastNameValue + " has been added!",
            icon: "success",
        });
    }
    else {
        form.submit();
    }
});

function checkName(name) {
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
    if (hours <= 0 || hours > 20 || typeof(hours) != "number") {
        return false;
    }
    else {
        return true;
    }
}