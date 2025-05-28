export type CategoryEnum = 0 | 1 | 2 | 3;

export const CategoryLabels = {
  0: "Residential",
  1: "Comercial",
  2: "Industrial",
  3: "Other",
} as const;

export type Lead = {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  phone: string;
  suburb: string;
  category: CategoryEnum;
  description: string;
  price: number;
  createdAt: string;
  status: number;
};

export type Tab = "invited" | "accepted";
