import { Component } from '@angular/core';
import {BlogPost, BlogPostService } from '../blog-post.service'
import { CommonModule } from '@angular/common';

@Component({
    selector: 'app-blog-post',
    imports: [CommonModule],
    templateUrl: './blog-post.component.html',
    styleUrl: './blog-post.component.css'
})
export class BlogPostComponent {
  // right now this is more of a BlogListComponent, but this is a good start
  posts: BlogPost[] = [];

  constructor(private blogService: BlogPostService) {}

  ngOnInit() {
    this.blogService.getAll().subscribe({
      next: data => this.posts = data,
      error: err => console.error('Failed to load blog posts')
    });
  }
}
