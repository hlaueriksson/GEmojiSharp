export function emojify(element) {
    DotNet.invokeMethodAsync('GEmojiSharp.Blazor', 'MarkupContent', element.innerHTML)
        .then(markup => element.innerHTML = markup);
};
