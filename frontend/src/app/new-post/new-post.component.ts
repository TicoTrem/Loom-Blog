import { Component, OnInit, OnDestroy, ViewChild, ElementRef } from '@angular/core';
import { FormsModule } from '@angular/forms';
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

  ngOnInit() {
    // Wait for view to initialize
    setTimeout(() => {
      this.simplemde = new EasyMDE({
        autofocus: true,
        autosave: {
          enabled: true,
          uniqueId: "MyUniqueID",
          delay: 1000,
          submit_delay: 5000,
          timeFormat: {
            locale: 'en-US',
            format: {
              year: 'numeric',
              month: 'long',
              day: '2-digit',
              hour: '2-digit',
              minute: '2-digit',
            },
          },
          text: "Autosaved: "
        },
        blockStyles: {
          bold: "__",
          italic: "_",
        },
        unorderedListStyle: "-",
        element: this.editorElement.nativeElement,
        forceSync: true,
        hideIcons: ["guide", "heading"],
        indentWithTabs: false,
        initialValue: "Hello world!",
        insertTexts: {
          horizontalRule: ["", "\n\n-----\n\n"],
          image: ["![](http://", ")"],
          link: ["[", "](https://)"],
          table: ["", "\n\n| Column 1 | Column 2 | Column 3 |\n| -------- | -------- | -------- |\n| Text     | Text      | Text     |\n\n"],
        },
        lineWrapping: false,
        minHeight: "500px",
        parsingConfig: {
          allowAtxHeaderWithoutSpace: true,
          strikethrough: false,
          underscoresBreakWords: true,
        },
        placeholder: "Type here...",

        previewClass: "my-custom-styling",

        promptURLs: true,
        promptTexts: {
          image: "Custom prompt for URL:",
          link: "Custom prompt for URL:",
        },
        shortcuts: {
          drawTable: "Cmd-Alt-T"
        },
        showIcons: ["code", "table"],
        spellChecker: false,
        status: false,
        styleSelectedText: false,
        sideBySideFullscreen: false,
        syncSideBySidePreviewScroll: false,
        tabSize: 4,
        toolbar: false,
        toolbarTips: false,
        toolbarButtonClassPrefix: "mde"
      });
    }, 0);
  }

  ngOnDestroy() {
    this.simplemde?.toTextArea();
  }
}
