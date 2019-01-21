
export class Product {
    id: string;
    name: string;
    description: string;
    categoryId: string;
}

export class ProductCategory {
    id: string;
    name: string;
}

export class Recipe {
    id: string;
    title: string;
    description: string;
    products: Product[];
    tags: Tag[];
    isPrivate: boolean;
    isMy: boolean;
}

export class RecipeUpdate {
    id: string;
    title: string;
    description: string;
    isPrivate: boolean;
    products: Product[];
    tags: Tag[];
}

export class Tag {
    id: string;
    text: string;
}

export class SingUpDTO {
    Email: string;
    Password: string;
    Name: string;
}
