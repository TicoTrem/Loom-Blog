import { Component } from '@angular/core';
import {BlogPost, BlogPostService } from '../blog-post.service'
import { ActivatedRoute } from '@angular/router';


@Component({
    selector: 'app-blog-post',
    standalone: true,
    imports: [],
    templateUrl: './blog-post.component.html',
    styleUrl: './blog-post.component.css'
})
export class BlogPostComponent {

  post: BlogPost | null = null;

  constructor(private blogService: BlogPostService, private route: ActivatedRoute) {}

  ngOnInit() {
    console.log(Number(this.route.snapshot.paramMap.get('id')));
    this.blogService.get(Number(this.route.snapshot.paramMap.get('id'))).subscribe({
      next: data => this.post = data,
      error: err => console.error('Failed to load blog post')
    });

    this.removeContentStyling();
  }

  removeContentStyling() {
    const content: HTMLCollectionOf<Element> = document.getElementsByClassName('content');
    content[0].classList.add('remove-content');
  }
}
