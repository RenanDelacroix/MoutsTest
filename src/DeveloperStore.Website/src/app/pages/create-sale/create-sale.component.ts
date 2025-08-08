import { Component, OnInit } from '@angular/core';
import { BranchService } from 'src/app/services/branch.service';
import { ProductService } from 'src/app/services/product.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-create-sale',
  templateUrl: './create-sale.component.html'
})

// ID fixo de usuário (Renan Leme)
export class CreateSaleComponent implements OnInit {
  sale: any = {
    customerId: '',   //ID fixo injetado
    branchId: '',
    items: [{ productId: '', quantity: 1 }]
  };

  products: any[] = [];
  branches: any[] = [];

  constructor(
    private productService: ProductService,
    private branchService: BranchService,
    private userService: UserService
  ) { }

  ngOnInit(): void {
    this.loadProducts();
    this.loadBranches();
    this.sale.customerId = this.userService.userId;
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
    // Implementar lógica para envio da venda
    console.log(this.sale);
  }
}
