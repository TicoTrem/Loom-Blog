import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { BlogPostListComponent } from '../blog-post-list/blog-post-list.component';

@Component({
  selector: 'app-home-page',
  imports: [BlogPostListComponent],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.css'
})
export class HomePageComponent {
}
