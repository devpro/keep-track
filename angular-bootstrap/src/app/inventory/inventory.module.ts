import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MovieComponent } from './movie/movie.component';
import { BookComponent } from './book/book.component';
import { CarComponent } from './car/car.component';

@NgModule({
  declarations: [
    MovieComponent,
    BookComponent,
    CarComponent
  ],
  imports: [
    CommonModule
  ]
})
export class InventoryModule { }
