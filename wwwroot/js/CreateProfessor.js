const form = document.getElementById("createProfessorform");
const submit = document.getElementById("createProfessorButton");
let firstName = document.getElementById("firstName");
let lastName = document.getElementById("lastName");

//showing an alert message when the form is submitted
submit.addEventListener("click", () => {
    let firstNameValue = firstName.value;
    let lastNameValue = lastName.value;

    if (checkName(firstNameValue) && checkName(lastNameValue)) {
        setTimeout(() => {
            form.submit();
        }, 1700);

        swal({
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