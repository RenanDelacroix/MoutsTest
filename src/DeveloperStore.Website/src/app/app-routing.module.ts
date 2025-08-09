// src/app/app-routing.module.ts
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateSaleComponent } from './pages/create-sale/create-sale.component';
import { ListSalesComponent } from './pages/list-sales/list-sales.component';

const routes: Routes = [
  { path: 'vendas', component: ListSalesComponent },
  { path: 'vendas/novo', component: CreateSaleComponent },
  { path: '', redirectTo: 'vendas', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
