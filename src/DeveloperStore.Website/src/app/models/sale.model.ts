export interface CreateSaleItem {
  productId: string;
  quantity: number;
}

export interface CreateSaleRequest {
  customerId: string;
  branchId: string;
  items: CreateSaleItem[];
}
