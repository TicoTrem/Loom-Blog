import { Component, OnInit, OnDestroy, ViewChild, ElementRef } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import EasyMDE from 'easymde';

@Component({
  selector: 'app-new-post',
  imports: [FormsModule],
  templateUrl: './new-post.component.html',
  styleUrl: './new-post.component.css'
})
export class NewPostComponent implements OnInit, OnDestroy {
  @ViewChild('editor') editorElement!: ElementRef<HTMLTextAreaElement>;
  private simplemde?: EasyMDE;
  blogPost: any

  ngOnInit() {
    // Wait for view to initialize
    // Even a delay of 0 makes it go after the current call stack.
    setTimeout(() => {
      this.simplemde = new EasyMDE({
        autofocus: true,
        blockStyles: {
          bold: "**",
          italic: "*",
        },
        unorderedListStyle: "-",
        element: this.editorElement.nativeElement,
        forceSync: true,
        lineWrapping: true,
        spellChecker: false,
        syncSideBySidePreviewScroll: false,
        tabSize: 4,
        toolbar: false,
        status: false,
      });
    }, 0);

    this.removeContentStyling();
  }

  onSubmit(form: NgForm) {
    console.log(form.value);
    console.log(this.blogPost);
  }

  ngOnDestroy() {
    this.simplemde?.toTextArea();
  }

  removeContentStyling() {
    const content: HTMLCollectionOf<Element> = document.getElementsByClassName('content');
    content[0].classList.add('remove-content');
  }
}
