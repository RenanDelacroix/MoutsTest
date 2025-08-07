import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CreateSaleComponent } from './pages/create-sale/create-sale.component';
import { ListSalesComponent } from './pages/list-sales/list-sales.component';

@NgModule({
  declarations: [
    AppComponent,
    CreateSaleComponent,
    ListSalesComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    AppRoutingModule
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
