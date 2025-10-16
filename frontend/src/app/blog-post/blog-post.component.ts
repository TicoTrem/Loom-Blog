import { ChangeDetectorRef, Component, ElementRef, ViewChild } from '@angular/core';
import {BlogPost, BlogPostService } from '../blog-post.service'
import { ActivatedRoute } from '@angular/router';
import { EasyMdeEditorComponent } from '../easymde-editor/easymde-editor.component';
import { Marked } from 'marked';
import { PartialObserver } from 'rxjs';


@Component({
    selector: 'app-blog-post',
    standalone: true,
    imports: [],
    templateUrl: './blog-post.component.html',
    styleUrl: './blog-post.component.css'
})
export class BlogPostComponent {

  post: BlogPost | null = null;
  marked: Marked | null = null;
  @ViewChild('content', { static: false, read: ElementRef }) contentDiv!: ElementRef<HTMLDivElement>


  constructor(private cdr: ChangeDetectorRef, private elementRef: ElementRef, private blogService: BlogPostService, private route: ActivatedRoute) {}

  ngOnInit() {
    this.marked = new Marked();
    this.blogService.get(Number(this.route.snapshot.paramMap.get('id'))).subscribe({
      next: data => this.afterPostReceived(data),
      error: err => console.error('Failed to load blog post')
    });

    this.removeContentStyling();

  }

  async afterPostReceived(data: BlogPost) {
    this.post = data
    // force angular to update page after the data comes back
    this.cdr.detectChanges();
    // contentDiv should be defined now
    if (this.marked !== null) {
      this.contentDiv.nativeElement.innerHTML = await this.marked.parse(this.contentDiv.nativeElement.innerHTML);
    }

  }

  removeContentStyling() {
    const content: HTMLCollectionOf<Element> = document.getElementsByClassName('content');
    content[0].classList.add('remove-content');
  }

  // renderMarkdown() {
  //   if (this.marked !== null) {
  //     console.log(this.contentDiv);
  //     let nativeEl : HTMLDivElement = this.contentDiv.nativeElement;
  //     // this.contentDiv.nativeElement.innerHTML = await this.marked.parse(this.contentDiv.nativeElement.innerHTML);
  //   }
  // }
}
