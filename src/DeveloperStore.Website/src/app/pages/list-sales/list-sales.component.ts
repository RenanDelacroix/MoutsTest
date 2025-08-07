import { Component, OnInit } from '@angular/core';
import { SaleService } from '../../services/sale.service';

@Component({
  selector: 'app-list-sales',
  templateUrl: './list-sales.component.html'
})
export class ListSalesComponent implements OnInit {
  sales: any[] = [];

  constructor(private saleService: SaleService) {}

  ngOnInit(): void {
    this.saleService.getSales().subscribe({
      next: (data: any) => { 
        this.sales = data.items || data;
      },
      error: (err: any) => { 
        console.error('Erro ao carregar vendas', err);
      }
    });
  }
}
