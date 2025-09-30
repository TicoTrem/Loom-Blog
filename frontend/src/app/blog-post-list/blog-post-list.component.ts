import { Component } from '@angular/core';
import { BlogPost, BlogPostService } from '../blog-post.service';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-blog-post-list',
  imports: [RouterModule],
  templateUrl: './blog-post-list.component.html',
  styleUrl: './blog-post-list.component.css'
})
export class BlogPostListComponent {

  posts: BlogPost[] = []

  constructor(private blogService: BlogPostService){}

  ngOnInit() {
    this.blogService.getAll().subscribe({next: (data) => this.posts = data})
  }

}
