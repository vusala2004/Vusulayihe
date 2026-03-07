// Inject footer HTML
document.addEventListener('DOMContentLoaded', function() {
  // Skip footer injection on admin page
  if (window.location.pathname.includes('admin.html')) return;

  const footerHTML = `
    <footer class="footer">
      <div class="footer-container">
        <div class="footer-section">
          <h4>Brûlé</h4>
          <p>Crafting exceptional coffee experiences since 2010. Quality beans, perfect brews, unforgettable moments.</p>
          <div class="social-icons">
            <a href="#" aria-label="Facebook">📘</a>
            <a href="#" aria-label="Instagram">📷</a>
            <a href="#" aria-label="Twitter">🐦</a>
            <a href="#" aria-label="LinkedIn">💼</a>
          </div>
        </div>
        <div class="footer-section">
          <h4>Quick Links</h4>
          <ul>
            <li><a href="index.html">Home</a></li>
            <li><a href="menu.html">Menu</a></li>
            <li><a href="about.html">About</a></li>
            <li><a href="contact.html">Contact</a></li>
          </ul>
        </div>
        <div class="footer-section">
          <h4>Customer Service</h4>
          <ul>
            <li><a href="cart.html">Cart</a></li>
            <li><a href="profile.html">My Account</a></li>
            <li><a href="reservations.html">Reservations</a></li>
            <li><a href="track.html">Track Order</a></li>
          </ul>
        </div>
        <div class="footer-section">
          <h4>Contact Info</h4>
          <p>123 Coffee Street<br>Bean City, BC 12345</p>
          <p>Phone: (555) 123-4567</p>
          <p>Email: hello@brule.com</p>
        </div>
      </div>
      <div class="footer-bottom">
        <p>&copy; 2024 Brûlé Coffee. All rights reserved.</p>
      </div>
    </footer>
  `;
  document.body.insertAdjacentElement('beforeend', footerHTML);
});