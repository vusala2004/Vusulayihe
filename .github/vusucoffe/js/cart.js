// Get cart from localStorage
function getCart() {
  const cart = localStorage.getItem('brule_cart');
  return cart ? JSON.parse(cart) : [];
}

// Save cart to localStorage
function saveCart(cart) {
  localStorage.setItem('brule_cart', JSON.stringify(cart));
}

// Add item to cart
function addItem(productId, quantity = 1, size = 'M') {
  const cart = getCart();
  const product = getProductById(productId);
  if (!product) return false;

  const existingItem = cart.find(item => item.id === productId && item.size === size);
  if (existingItem) {
    existingItem.quantity += quantity;
  } else {
    cart.push({
      id: product.id,
      name: product.name,
      price: product.price,
      size: size,
      quantity: quantity,
      image: product.image
    });
  }
  saveCart(cart);
  return true;
}

// Remove item from cart
function removeItem(productId, size = 'M') {
  const cart = getCart();
  const updatedCart = cart.filter(item => !(item.id === productId && item.size === size));
  saveCart(updatedCart);
}

// Update quantity of item in cart
function updateQty(productId, size = 'M', quantity) {
  const cart = getCart();
  const item = cart.find(item => item.id === productId && item.size === size);
  if (item) {
    item.quantity = quantity;
    if (item.quantity <= 0) {
      removeItem(productId, size);
    } else {
      saveCart(cart);
    }
  }
}

// Get total price of cart
function getTotal() {
  const cart = getCart();
  return cart.reduce((total, item) => total + (item.price * item.quantity), 0);
}

// Get cart count
function getCartCount() {
  const cart = getCart();
  return cart.reduce((count, item) => count + item.quantity, 0);
}

// Clear cart
function clearCart() {
  localStorage.removeItem('brule_cart');
}