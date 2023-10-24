const sidebarBtn = document.querySelector('.sidebar-btn');
const layout = document.querySelector('.layout');
const closeLayoutBtn = document.querySelector('.close-sidebar-btn');
sidebarBtn.addEventListener('click', () => {
    layout.classList.add('active');
})
closeLayoutBtn.addEventListener('click', () => {
    layout.classList.remove('active');
})

