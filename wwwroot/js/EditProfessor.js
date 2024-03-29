const form = document.getElementById("editProfessorform");
const editButton = document.getElementById("editProfessorButton");
const deleteButton = document.getElementById("deleteProfessorButton");
let firstName = document.getElementById("firstName");
let lastName = document.getElementById("lastName");

//showing an alert message when the form is submitted - edit professor
editButton.addEventListener("click", () => {
    let firstNameValue = firstName.value;
    let lastNameValue = lastName.value;

    setTimeout(() => {
        form.submit();
    }, 2000);

    swal({
        title: "Good job!",
        text: "Success! The data for " + firstNameValue + " " + lastNameValue + " has been updated!",
        icon: "success",
    });
});

//showing an alert message when the form is submitted - delete professor
deleteButton.addEventListener("click", () => {
    let firstNameValue = firstName.value;
    let lastNameValue = lastName.value;

    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover the data!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                swal("Professor " + firstNameValue + " " + lastNameValue + " has been deleted!", {
                    icon: "success",
                });
                setTimeout(() => {
                    form.submit();
                }, 2000);
            }
            else {
                swal("The professor was not deleted!");
            }
        });
});