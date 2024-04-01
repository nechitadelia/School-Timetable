const form = document.getElementById("createProfessorform");
const submit = document.getElementById("createProfessorButton");
let firstName = document.getElementById("firstName");
let lastName = document.getElementById("lastName");

//showing an alert message when the form is submitted
submit.addEventListener("click", () => {
    let firstNameValue = firstName.value;
    let lastNameValue = lastName.value;

    setTimeout(() => {
        form.submit();
    }, 1700);

    swal({
        title: "Good job!",
        text: "Success! Professor " + firstNameValue + " " + lastNameValue + " has been added!",
        icon: "success",
    });
});