module.exports = function (context) {
    context.bindings.translated = context.bindings.documents.map(d =>
        Object.assign({
            "translated": d.text + " in French"
        }, d))[0];

    context.done();
};