const toggler = document.querySelector(".btn");
const sidebar = document.querySelector("#sidebar");
const main = document.querySelector(".main");

toggler.addEventListener("click", function (e) {
    sidebar.classList.toggle("collapsed");
    main.classList.toggle("sidebar-open");
});

document.addEventListener("click", function (e) {
    if (!sidebar.contains(e.target) && !toggler.contains(e.target) && window.innerWidth <= 740) {
        sidebar.classList.add("collapsed");
        main.classList.remove("sidebar-open");
    }
});

// Ẩn sidebar khi kích thước màn hình thay đổi
window.addEventListener("resize", function () {
    if (window.innerWidth > 740) {
        sidebar.classList.remove("collapsed");
        main.classList.remove("sidebar-open");
    }
});