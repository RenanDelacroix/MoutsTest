import { Component, OnInit } from '@angular/core';
import { BranchService } from 'src/app/services/branch.service';
import { ProductService } from 'src/app/services/product.service';
import { SaleService } from 'src/app/services/sale.service';
import { UserService } from 'src/app/services/user.service';

interface SaleItem {
  productId: string;
  quantity: number;
}

@Component({
  selector: 'app-create-sale',
  templateUrl: './create-sale.component.html'
})
export class CreateSaleComponent implements OnInit {
  sale: { customerId: string; branchId: string; items: SaleItem[] } = {
    customerId: '',
    branchId: '',
    items: []
  };

  products: any[] = [];
  branches: any[] = [];
  selectedProductId: string = '';
  selectedQuantity: number = 1;

  constructor(
    private productService: ProductService,
    private branchService: BranchService,
    private saleService: SaleService,
    private userService: UserService
  ) { }

  ngOnInit(): void {
    // Sempre usa o mesmo cliente fixo (do usuário logado)
    this.sale.customerId = this.userService.userId;

    this.loadProducts();
    this.loadBranches();
  }

  loadProducts() {
    this.productService.getProducts(1, 10, 'name', 'desc').subscribe({
      next: (res) => (this.products = res.items),
      error: (err) => console.error('Erro ao carregar produtos', err)
    });
  }

  loadBranches() {
    this.branchService.getBranches().subscribe({
      next: (res) => (this.branches = res),
      error: (err) => console.error('Erro ao carregar filiais', err)
    });
  }

  addProductToPanel() {
    if (!this.selectedProductId || this.selectedQuantity <= 0) return;

    const existingItem = this.sale.items.find(
      (i: SaleItem) => i.productId === this.selectedProductId
    );

    if (existingItem) {
      existingItem.quantity += this.selectedQuantity;
    } else {
      this.sale.items.push({
        productId: this.selectedProductId,
        quantity: this.selectedQuantity
      });
    }

    // Reset campos
    this.selectedProductId = '';
    this.selectedQuantity = 1;
  }

  removeItem(index: number) {
    this.sale.items.splice(index, 1);
  }

  getProductName(productId: string): string {
    const product = this.products.find((p) => p.id === productId);
    return product ? product.name : '';
  }

  getProductPrice(productId: string): number {
    const product = this.products.find((p) => p.id === productId);
    return product ? product.price : 0;
  }

  submit() {
    if (!this.sale.branchId || this.sale.items.length === 0) {
      alert('Selecione uma filial e adicione pelo menos um produto.');
      return;
    }

    const payload = {
      customerId: this.sale.customerId,
      branchId: this.sale.branchId,
      items: this.sale.items.map((item) => ({
        productId: item.productId,
        quantity: item.quantity
      }))
    };

    this.saleService.createSale(payload).subscribe({
      next: () => {
        alert('Venda registrada com sucesso!');
        // Reset após sucesso
        this.sale.branchId = '';
        this.sale.items = [];
      },
      error: (err) => {
        if (err.status === 400 && err.error) {
          alert(`Erro ao criar venda: ${err.error}`);
        } else {
          alert('Erro ao criar venda: Ocorreu um erro inesperado.');
        }
        console.error('Erro ao criar venda', err);
      }
    });
  }
}
