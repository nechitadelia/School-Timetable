
//navbar styling for phone - dropdown settings for navbar (User info list)
let dropList = document.querySelector(".dropdown-settings");
let title = document.querySelector(".userNavbar");
let coursesLink = document.querySelector(".link-courses");

title.addEventListener("click", function () {
    if (window.innerWidth < 769) {
        dropList.classList.toggle("dropdown-active");
        coursesLink.classList.toggle("courses-active");

        title.style.display = "block";
        title.style.textAlign = "center";
        title.style.color = "#ff9774";
    }

    if (dropList.classList.contains("dropdown-active") == false) {
        title.style.color = "#fff";
    }
});

//navbar styling for phone - click on burger icon
let loginLink = document.getElementById("login-link");

function navSlide() {
    const burger = document.querySelector(".burger");
    const nav = document.querySelector(".nav-links");
    const navLinks = document.querySelectorAll(".link");

    //if (window.innerWidth <= 900) {
    //    burger.classList.add("burger-visible");
    //}
    //else {
    //    burger.classList.remove("burger-visible");
    //}

    if (loginLink != null && typeof(loginLink) != "undefined") {
        burger.style.display = "none";
    } else {
        burger.style.display = "block";
    }

    burger.addEventListener("click", () => {
        //toggle nav
        nav.classList.toggle("nav-active");
        if (dropList.class = "dropdown-active") {
            dropList.classList.remove("dropdown-active");
        }

        //animate links
        navLinks.forEach((link, index) => {
            if (link.style.animation) {
                link.style.animation = ''
            } else {
                link.style.animation = `navLinkFade 0.5s ease forwards ${index / 7 + 0.2}s`;
            }
        });

        //burger animation
        burger.classList.toggle("toggle");
    });

    if (loginLink == null) {
        
    }
}

navSlide();