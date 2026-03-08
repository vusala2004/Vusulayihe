const products = [
  {
    id: 1,
    name: 'Espresso',
    category: 'Espresso',
    price: 3.50,
    description: 'A bold, concentrated coffee shot made from finely ground beans.',
    image: '../img/Espressocoffee.jpg'
  },
  {
    id: 2,
    name: 'Cappuccino',
    category: 'Espresso',
    price: 4.50,
    description: 'Espresso topped with steamed milk foam and a sprinkle of cocoa.',
    image: '../img/Cappuccino.jpg'
  },
  {
    id: 3,
    name: 'Latte',
    category: 'Espresso',
    price: 4.75,
    description: 'Smooth espresso with steamed milk and a thin layer of foam.',
    image: '../img/Latte.jpg'
  },
  {
    id: 4,
    name: 'Americano',
    category: 'Espresso',
    price: 3.75,
    description: 'Espresso diluted with hot water for a milder taste.',
    image: '../img/Americano.jpg'
  },
  {
    id: 5,
    name: 'Cold Brew',
    category: 'Cold Brew',
    price: 4.00,
    description: 'Smooth, low-acid coffee brewed cold for 12+ hours.',
    image: '../img/ColdBrew.jpg'
  },
  {
    id: 6,
    name: 'Iced Latte',
    category: 'Cold Brew',
    price: 5.00,
    description: 'Chilled espresso with cold milk over ice.',
    image: '../img/IcedLatte.jpg'
  },
  {
    id: 7,
    name: 'Nitro Cold Brew',
    category: 'Cold Brew',
    price: 5.50,
    description: 'Cold brew infused with nitrogen for a creamy texture.',
    image: '../img/NitroColdBrew.jpg'
  },
  {
    id: 8,
    name: 'Green Tea',
    category: 'Tea',
    price: 3.25,
    description: 'Refreshing green tea with antioxidant properties.',
    image: '../img/Greentea.jpg'
  },
  {
    id: 9,
    name: 'Chai Latte',
    category: 'Tea',
    price: 4.50,
    description: 'Spiced tea blend with steamed milk and honey.',
    image: '../img/ChaiLatte.jpg'
  },
  {
    id: 10,
    name: 'Matcha Latte',
    category: 'Tea',
    price: 4.75,
    description: 'Premium matcha powder whisked with steamed milk.',
    image: '../img/MatchaLatte.jpg'
  },
  {
    id: 11,
    name: 'Croissant',
    category: 'Pastries',
    price: 3.00,
    description: 'Buttery, flaky pastry perfect with your morning coffee.',
    image: '../img/Croissant.jpg'
  },
  {
    id: 12,
    name: 'Blueberry Muffin',
    category: 'Pastries',
    price: 3.50,
    description: 'Moist muffin packed with fresh blueberries.',
    image: '../img/BlueberryMuffin.jpg'
  },
  {
    id: 13,
    name: 'Chocolate Chip Cookie',
    category: 'Pastries',
    price: 2.75,
    description: 'Classic cookie with gooey chocolate chips.',
    image: '../img/ChocolateChipCookie.jpg'
  },
  {
    id: 14,
    name: 'Pumpkin Spice Latte',
    category: 'Seasonal',
    price: 5.25,
    description: 'Seasonal favorite with pumpkin spice and whipped cream.',
    image: '../img/PumpkinSpiceLatte.jpg'
  },
  {
    id: 15,
    name: 'Gingerbread Cookie',
    category: 'Seasonal',
    price: 3.25,
    description: 'Spiced cookie with festive gingerbread flavors.',
    image: '../img/GingerbreadCookie.jpg'
  }
];

// Function to get products by category
function getProductsByCategory(category) {
  if (category === 'All') return products;
  return products.filter(product => product.category === category);
}

// Function to get product by id
function getProductById(id) {
  return products.find(product => product.id === parseInt(id));
}

// Function to search products by name
function searchProducts(query) {
  return products.filter(product =>
    product.name.toLowerCase().includes(query.toLowerCase())
  );
}

// Function to sort products
function sortProducts(products, sortBy) {
  const sorted = [...products];
  switch (sortBy) {
    case 'price-low':
      return sorted.sort((a, b) => a.price - b.price);
    case 'price-high':
      return sorted.sort((a, b) => b.price - a.price);
    case 'popular':
      // For simplicity, sort by id (assuming lower id = more popular)
      return sorted.sort((a, b) => a.id - b.id);
    default:
      return sorted;
  }
}