import { Component, OnInit } from '@angular/core';
import { SaleService } from '../../services/sale.service';
import { UserService } from '../../services/user.service';
import { animate, state, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-list-sales',
  templateUrl: './list-sales.component.html',
  styleUrls: ['./list-sales.component.css'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0', visibility: 'hidden' })),
      state('expanded', style({ height: '*', visibility: 'visible' })),
      transition('expanded <=> collapsed', animate('200ms ease-in-out')),
    ])
  ]
})
export class ListSalesComponent implements OnInit {
  sales: any[] = [];
  totalCount = 0;
  userName = '';

  expandedRows: Set<string> = new Set();
  saleDetails: { [key: string]: any[] } = {};
  loadingDetails: { [key: string]: boolean } = {};

  displayedColumns = ['number', 'customer', 'branch', 'subtotal', 'discount', 'total', 'status', 'createdAt', 'actions'];
  allDisplayedColumns: string[] = [];

  pageSize = 10;
  pageNumber = 1;

  constructor(
    private saleService: SaleService,
    public userService: UserService
  ) { }

  ngOnInit(): void {
    this.userName = this.userService.userName;
    this.allDisplayedColumns = ['expand', ...this.displayedColumns];
    this.loadSales();
  }

  onPageChange(event: any) {
    this.pageNumber = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.loadSales();
  }

  getSubtotal(sale: any): number {
    return sale.items.reduce(
      (sum: number, item: any) =>
        sum + (item.subtotal || (item.unitPrice * item.quantity)),
      0
    );
  }

  loadSales() {
    this.saleService.getSales(this.pageNumber, this.pageSize).subscribe({
      next: (data) => {
        this.sales = data.items.map((sale: any) => ({
          ...sale,
          totalComDesconto: sale.total - sale.discount
        }));
        this.totalCount = data.totalCount;
      },
      error: (err) => {
        console.error('Erro ao carregar vendas', err);
      }
    });
  }

  cancelSale(id: string) {
    if (!confirm('Tem certeza que deseja cancelar esta venda?')) return;

    this.saleService.cancelSale(id).subscribe({
      next: (res) => {
        alert(res.message);
        this.loadSales();
      },
      error: (err) => {
        console.error('Erro ao cancelar venda', err);
        alert('Erro ao cancelar a venda.');
      }
    });
  }

  paySale(id: string) {
    if (!confirm('Tem certeza que deseja marcar esta venda como paga?')) return;

    this.saleService.paySale(id).subscribe({
      next: (res) => {
        alert(res.message);
        this.loadSales();
      },
      error: (err) => {
        console.error('Erro ao pagar venda', err);
        alert('Erro ao pagar a venda.');
      }
    });
  }

  toggleSaleDetails(saleId: string) {
    if (this.expandedRows.has(saleId)) {
      this.expandedRows.delete(saleId);
    } else {
      this.expandedRows.add(saleId);
      if (!this.saleDetails[saleId]) {
        this.loadingDetails[saleId] = true;
        this.saleService.getSaleById(saleId).subscribe({
          next: (data) => {
            this.saleDetails[saleId] = data.items || [];
            this.loadingDetails[saleId] = false;
          },
          error: (err) => {
            console.error('Erro ao carregar detalhes da venda', err);
            this.loadingDetails[saleId] = false;
          }
        });
      }
    }
  }

  isExpanded(saleId: string): boolean {
    return this.expandedRows.has(saleId);
  }
}
