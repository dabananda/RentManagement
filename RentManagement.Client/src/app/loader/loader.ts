import { Component, inject } from '@angular/core';
import { LoaderService } from '../_services/loader.service';
import { CommonModule} from '@angular/common';

@Component({
  selector: 'app-loader',
  imports: [CommonModule],
  templateUrl: './loader.html',
  styleUrl: './loader.css',
})
export class Loader {
  loader = inject(LoaderService);
}
