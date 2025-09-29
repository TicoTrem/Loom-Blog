import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { BlogPostComponent } from './blog-post/blog-post.component';
import { BrowserModule } from '@angular/platform-browser';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, BlogPostComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'frontend';
}
