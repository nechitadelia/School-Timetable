const form = document.getElementById("createProfessorform");
const createButton = document.getElementById("createProfessorButton");
let firstName = document.getElementById("firstName");
let lastName = document.getElementById("lastName");
let maxHours = document.getElementById("MaxHours");

//showing an alert message when the form is submitted - create professor
createButton.addEventListener("click", () => {
    let firstNameValue = firstName.value;
    let lastNameValue = lastName.value;
    let maxHoursValue = maxHours.value;

    if (checkName(firstNameValue) && checkName(lastNameValue) && checkMaxHours(maxHoursValue)) {
        setTimeout(() => {
            form.submit();
        }, 1700);

        Swal.fire({
            title: "Success!",
            text: "Professor " + firstNameValue + " " + lastNameValue + " has been added!",
            icon: "success",
        });
    }
    else {
        form.submit();
    }
});