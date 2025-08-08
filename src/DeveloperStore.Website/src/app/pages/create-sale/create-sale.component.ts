import { Component, OnInit } from '@angular/core';
import { BranchService } from 'src/app/services/branch.service';
import { ProductService } from 'src/app/services/product.service';
import { SaleService } from 'src/app/services/sale.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-create-sale',
  templateUrl: './create-sale.component.html'
})
export class CreateSaleComponent implements OnInit {
  sale: any = {
    customerId: '',
    branchId: '',
    items: [{ productId: '', quantity: 1 }]
  };

  products: any[] = [];
  branches: any[] = [];

  constructor(
    private productService: ProductService,
    private branchService: BranchService,
    private saleService: SaleService,
    private userService: UserService
  ) { }

  ngOnInit(): void {
    // Sempre usa o mesmo cliente fixo (Renan Leme)
    this.sale.customerId = this.userService.userId;

    this.loadProducts();
    this.loadBranches();
  }

  loadProducts() {
    this.productService.getProducts(1, 10, 'name', 'desc').subscribe({
      next: (res) => this.products = res.items,
      error: (err) => console.error('Erro ao carregar produtos', err)
    });
  }

  loadBranches() {
    this.branchService.getBranches().subscribe({
      next: (res) => this.branches = res,
      error: (err) => console.error('Erro ao carregar filiais', err)
    });
  }

  addItem() {
    this.sale.items.push({ productId: '', quantity: 1 });
  }

  submit() {
    // Monta o payload e envia para a API
    const payload = {
      customerId: this.sale.customerId,
      branchId: this.sale.branchId,
      items: this.sale.items.map((item: any) => ({
        productId: item.productId,
        quantity: item.quantity
      }))
    };

    console.log('Enviando venda:', payload);

    this.saleService.createSale(payload).subscribe({
      next: (res) => {
        console.log('Venda criada com sucesso!', res);
        alert('Venda registrada com sucesso!');
      },
      error: (err) => {
        console.error('Erro ao criar venda', err);
        alert('Erro ao criar venda.');
      }
    });
  }
}
