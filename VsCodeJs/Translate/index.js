module.exports = function (context) {
    context.binding.translatedDocs = context.binding.documents.map(d =>
        Object.assign({
            "translated": d.text + " in French"
        }, d));
    context.done();
};