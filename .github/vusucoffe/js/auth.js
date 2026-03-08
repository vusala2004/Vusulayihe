// Seed admin user if not exists
if (!localStorage.getItem('brule_user')) {
  const adminUser = {
    name: 'Admin',
    email: 'admin@brule.com',
    password: 'admin123',
    role: 'admin'
  };
  localStorage.setItem('brule_user', JSON.stringify(adminUser));
}

// Get current user from localStorage
function getCurrentUser() {
  const user = localStorage.getItem('brule_user');
  return user ? JSON.parse(user) : null;
}

// Logout function
function logout() {
  localStorage.removeItem('brule_user');
  window.location.href = 'login.html';
}

// Require authentication with optional role
function requireAuth(requiredRole = null) {
  const user = getCurrentUser();
  if (!user) {
    window.location.href = 'login.html';
    return false;
  }
  if (requiredRole && user.role !== requiredRole) {
    window.location.href = 'index.html';
    return false;
  }
  return true;
}

// Login function
function login(email, password) {
  const user = getCurrentUser();
  if (user && user.email === email && user.password === password) {
    return true;
  }
  return false;
}

// Register function
function register(name, email, password) {
  const existingUser = getCurrentUser();
  if (existingUser && existingUser.email === email) {
    return false; // User already exists
  }
  const newUser = {
    name: name,
    email: email,
    password: password,
    role: 'user'
  };
  localStorage.setItem('brule_user', JSON.stringify(newUser));
  return true;
}

// Update user profile
function updateUser(updates) {
  const user = getCurrentUser();
  if (user) {
    const updatedUser = { ...user, ...updates };
    localStorage.setItem('brule_user', JSON.stringify(updatedUser));
    return true;
  }
  return false;
}