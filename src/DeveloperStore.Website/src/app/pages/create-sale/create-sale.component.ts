import { Component } from '@angular/core';
import { SaleService } from '../../services/sale.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-sale',
  templateUrl: './create-sale.component.html'
})
export class CreateSaleComponent {
  sale = {
    customerId: '',
    branchId: '',
    items: [{ productId: '', quantity: 1 }]
  };

  constructor(private saleService: SaleService, private router: Router) {}

  addItem(): void {
    this.sale.items.push({ productId: '', quantity: 1 });
  }

  submit(): void {
    this.saleService.createSale(this.sale).subscribe({
      next: (res: any) => { 
        console.log('Venda criada com sucesso', res);
        this.router.navigate(['/vendas']);
      },
      error: (err: any) => { 
        console.error('Erro ao criar venda', err);
      }
    });
  }
}
