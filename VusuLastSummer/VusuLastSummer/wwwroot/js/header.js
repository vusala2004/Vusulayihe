// Inject header HTML
document.addEventListener('DOMContentLoaded', function() {
  // Skip header injection on admin page
  if (window.location.pathname.includes('admin.html')) return;

//    const headerHTML = `
//  <nav class="navbar" id="navbar">
//    <div class="navbar-container">
//      <a href="/" class="logo">Last Summer 🐦‍⬛</a>

//      <ul class="nav-links" id="nav-links">
//        <li><a href="/" data-key="home">Home</a></li>
//        <li><a href="/Menu" data-key="menu">Menu</a></li>
//        <li><a href="/About" data-key="about">About</a></li>
//        <li><a href="/Blog" data-key="blog">Blog</a></li>
//        <li><a href="/Contact" data-key="contact">Contact</a></li>
//        <li><a href="/Reservations" data-key="reservations">Reservations</a></li>
//      </ul>

//      <div class="flex gap-2 align-center">

//        <div class="cart-icon" onclick="window.location.href='/Cart'">
//          🛒
//          <span class="cart-badge" id="cart-badge" style="display: none;"></span>
//        </div>

//        <div class="user-icon" onclick="toggleUserMenu()">👤</div>

//        <div class="hamburger" id="hamburger" onclick="toggleMobileMenu()">
//          <span></span>
//          <span></span>
//          <span></span>
//        </div>

//      </div>
//    </div>

//    <div id="user-menu" class="hidden"
//         style="position: absolute; top: 100%; right: 20px; background: var(--cream); padding: 1rem; border-radius: 5px; box-shadow: 0 4px 15px var(--shadow);">
//      <a href="/Profile">Profile</a>
//      <a href="#" onclick="logout()">Logout</a>
//    </div>
//  </nav>
//`;

 /* document.body.insertAdjacentHTML('afterbegin', headerHTML);*/

  updateCartBadge();
  updateUserMenu();

  // ✅ Language auto load
  const savedLang = localStorage.getItem("language") || "en";
  const langSelect = document.getElementById("language-select");

  if (langSelect) {
    langSelect.value = savedLang;
    if (typeof changeLanguage === "function") {
      changeLanguage(savedLang);
    }
  }
});

// Update cart badge
function updateCartBadge() {
  const badge = document.getElementById('cart-badge');
  if (badge) {
    const count = getCartCount();
    if (count > 0) {
      badge.textContent = count;
      badge.style.display = 'flex';
    } else {
      badge.style.display = 'none';
    }
  }
}

// Toggle mobile menu
function toggleMobileMenu() {
  const navLinks = document.getElementById('nav-links');
  const hamburger = document.getElementById('hamburger');
  if (navLinks && hamburger) {
    navLinks.classList.toggle('active');
    hamburger.classList.toggle('open');
  }
}

// Toggle user menu
function toggleUserMenu() {
  const userMenu = document.getElementById('user-menu');
  if (userMenu) {
    const user = getCurrentUser();
    if (user) {
      userMenu.classList.toggle('hidden');
    } else {
      window.location.href = 'login.html';
    }
  }
}

// Update user menu visibility
function updateUserMenu() {
  const userIcon = document.querySelector('.user-icon');
  if (userIcon) {
    const user = getCurrentUser();
    if (user) {
      userIcon.style.display = 'block';
    } else {
      userIcon.style.display = 'none';
    }
  }
}