import { useEffect, useState } from "react";
import api from "../services/api";
import "./Produtos.css";

function Produtos() {

    const [produtos, setProdutos] = useState([]);
    const [nome, setNome] = useState("");
    const [descricao, setDescricao] = useState("");
    const [preco, setPreco] = useState("");
    const [editandoId, setEditandoId] = useState(null);

    useEffect(() => {
        listarProdutos();
    }, []);

    async function listarProdutos() {
        const token = localStorage.getItem("token");
        const response = await api.get("/produto", {
            headers: { Authorization: `Bearer ${token}` }
        });
        setProdutos(response.data);
    }

    async function salvarProduto() {
        const token = localStorage.getItem("token");

        if (editandoId) {
            await api.put(`/produto/${editandoId}`, { codigo: editandoId, nome, descricao, preco: parseFloat(preco) }, {
                headers: { Authorization: `Bearer ${token}` }
            });
            setEditandoId(null);
        } else {
            await api.post("/produto", { nome, descricao, preco: parseFloat(preco) }, {
                headers: { Authorization: `Bearer ${token}` }
            });
        }

        setNome("");
        setDescricao("");
        setPreco("");
        listarProdutos();
    }

    async function excluirProduto(codigo) {
        const token = localStorage.getItem("token");
        await api.delete(`/produto/${codigo}`, {
            headers: { Authorization: `Bearer ${token}` }
        });
        listarProdutos();
    }

    function editarProduto(produto) {
        setEditandoId(produto.codigo);
        setNome(produto.nome);
        setDescricao(produto.descricao);
        setPreco(produto.preco);
    }

    return (
        <div className="container">
            <h1>Produtos</h1>

            <div className="formulario">
                <input placeholder="Nome" value={nome} onChange={(e) => setNome(e.target.value)} />
                <input placeholder="Descrição" value={descricao} onChange={(e) => setDescricao(e.target.value)} />
                <input placeholder="Preço" value={preco} onChange={(e) => setPreco(e.target.value)} />
                <button onClick={salvarProduto}>{editandoId ? "Atualizar" : "Salvar"}</button>
            </div>

            <table>
                <thead>
                    <tr>
                        <th>Nome</th>
                        <th>Descrição</th>
                        <th>Preço</th>
                        <th>Ações</th>
                    </tr>
                </thead>
                <tbody>
                    {produtos.map((produto) => (
                        <tr key={produto.codigo}>
                            <td>{produto.nome}</td>
                            <td>{produto.descricao}</td>
                            <td>R$ {produto.preco}</td>
                            <td>
                                <button onClick={() => editarProduto(produto)}>Editar</button>
                                <button onClick={() => excluirProduto(produto.codigo)}>Excluir</button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}

export default Produtos;