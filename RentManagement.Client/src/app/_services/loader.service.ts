import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class LoaderService {
  loading = signal(false);
  counter = 0;

  show(): void {
    this.loading.set(true);
    this.counter++;
  }

  hide(): void {
    this.counter--;
    if (this.counter <= 0) {
      this.counter = 0;
      this.loading.set(false);
    }
  }
}
