let deleteButtons = document.querySelectorAll(".delete-subject-button");
let existingProfessors = document.querySelectorAll(".existingProfessors-error");

for (let i = 0; i < deleteButtons.length; i++) {
    deleteButtons[i].addEventListener("click", () => {
        if (existingProfessors[i].innerHTML != "No professors") {
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "There are professors associated with this subject. You need to delete those professors first."
            });
        }
    });
}
