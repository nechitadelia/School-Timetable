﻿const form = document.getElementById("editProfessorform");
const editButton = document.getElementById("editProfessorButton");
let firstName = document.getElementById("firstName");
let lastName = document.getElementById("lastName");

//showing an alert message when the form is submitted - edit professor
editButton.addEventListener("click", () => {
    let firstNameValue = firstName.value;
    let lastNameValue = lastName.value;

    if (checkName(firstNameValue) && checkName(lastNameValue)) {
        setTimeout(() => {
            form.submit();
        }, 1700);

        Swal.fire({
            title: "Success!",
            text: "The data for " + firstNameValue + " " + lastNameValue + " has been updated!",
            icon: "success",
        });
    }
    else {
        form.submit();
    }
});