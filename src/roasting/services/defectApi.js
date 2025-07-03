import api from '../../shared/config/api.js';

/**
 * Servicio de API para la gestión de defectos
 * 
 * Este servicio proporciona métodos para interactuar con el backend
 * y gestionar los defectos del sistema CafeLab.
 * 
 * Características implementadas:
 * - CRUD completo de defectos
 * - Filtrado por categoría y severidad
 * - Estadísticas de defectos
 * - Manejo de errores automático
 * - Autenticación JWT automática
 */

/**
 * Obtener todos los defectos del sistema
 * 
 * @returns {Promise<Array>} Lista de defectos
 * @throws {Error} Si hay un error en la petición HTTP
 */
export async function getDefects() {
  try {
    console.log('🔍 Obteniendo lista de defectos...');
    const response = await api.get('/defects');
    console.log('✅ Defectos obtenidos exitosamente:', response.data);
    return response.data;
  } catch (error) {
    console.error('❌ Error al obtener defectos:', error);
    throw error;
  }
}

/**
 * Obtener un defecto específico por su ID
 * 
 * @param {number} id - ID del defecto a obtener
 * @returns {Promise<Object>} Datos del defecto
 * @throws {Error} Si hay un error en la petición HTTP
 */
export async function getDefectById(id) {
  try {
    console.log(`🔍 Obteniendo defecto con ID: ${id}`);
    const response = await api.get(`/defects/${id}`);
    console.log('✅ Defecto obtenido exitosamente:', response.data);
    return response.data;
  } catch (error) {
    console.error(`❌ Error al obtener defecto ${id}:`, error);
    throw error;
  }
}

/**
 * Crear un nuevo defecto
 * 
 * @param {Object} defectData - Datos del defecto a crear
 * @param {string} defectData.name - Nombre del defecto
 * @param {string} defectData.description - Descripción del defecto
 * @param {string} defectData.category - Categoría del defecto (Primario/Secundario)
 * @param {string} defectData.severity - Severidad del defecto (Bajo/Medio/Alto/Crítico)
 * @param {string} defectData.solution - Solución recomendada
 * @returns {Promise<Object>} Defecto creado
 * @throws {Error} Si hay un error en la petición HTTP
 */
export async function createDefect(defectData) {
  try {
    console.log('➕ Creando nuevo defecto:', defectData);
    const response = await api.post('/defects', defectData);
    console.log('✅ Defecto creado exitosamente:', response.data);
    return response.data;
  } catch (error) {
    console.error('❌ Error al crear defecto:', error);
    throw error;
  }
}

/**
 * Actualizar un defecto existente
 * 
 * @param {number} id - ID del defecto a actualizar
 * @param {Object} defectData - Nuevos datos del defecto
 * @returns {Promise<Object>} Defecto actualizado
 * @throws {Error} Si hay un error en la petición HTTP
 */
export async function updateDefect(id, defectData) {
  try {
    console.log(`✏️ Actualizando defecto ${id}:`, defectData);
    const response = await api.put(`/defects/${id}`, defectData);
    console.log('✅ Defecto actualizado exitosamente:', response.data);
    return response.data;
  } catch (error) {
    console.error(`❌ Error al actualizar defecto ${id}:`, error);
    throw error;
  }
}

/**
 * Eliminar un defecto
 * 
 * @param {number} id - ID del defecto a eliminar
 * @returns {Promise<Object>} Respuesta de confirmación
 * @throws {Error} Si hay un error en la petición HTTP
 */
export async function deleteDefect(id) {
  try {
    console.log(`🗑️ Eliminando defecto ${id}`);
    const response = await api.delete(`/defects/${id}`);
    console.log('✅ Defecto eliminado exitosamente');
    return response.data;
  } catch (error) {
    console.error(`❌ Error al eliminar defecto ${id}:`, error);
    throw error;
  }
}

/**
 * Obtener defectos por categoría
 * 
 * @param {string} category - Categoría a filtrar (Primario/Secundario)
 * @returns {Promise<Array>} Lista de defectos filtrados
 * @throws {Error} Si hay un error en la petición HTTP
 */
export async function getDefectsByCategory(category) {
  try {
    console.log(`🔍 Obteniendo defectos por categoría: ${category}`);
    const response = await api.get(`/defects/category/${category}`);
    console.log('✅ Defectos por categoría obtenidos:', response.data);
    return response.data;
  } catch (error) {
    console.error(`❌ Error al obtener defectos por categoría ${category}:`, error);
    throw error;
  }
}

/**
 * Obtener defectos por severidad
 * 
 * @param {string} severity - Severidad a filtrar (Bajo/Medio/Alto/Crítico)
 * @returns {Promise<Array>} Lista de defectos filtrados
 * @throws {Error} Si hay un error en la petición HTTP
 */
export async function getDefectsBySeverity(severity) {
  try {
    console.log(`🔍 Obteniendo defectos por severidad: ${severity}`);
    const response = await api.get(`/defects/severity/${severity}`);
    console.log('✅ Defectos por severidad obtenidos:', response.data);
    return response.data;
  } catch (error) {
    console.error(`❌ Error al obtener defectos por severidad ${severity}:`, error);
    throw error;
  }
}

/**
 * Obtener defectos críticos (alta severidad)
 * 
 * @returns {Promise<Array>} Lista de defectos críticos
 * @throws {Error} Si hay un error en la petición HTTP
 */
export async function getCriticalDefects() {
  try {
    console.log('🚨 Obteniendo defectos críticos...');
    const response = await api.get('/defects/critical');
    console.log('✅ Defectos críticos obtenidos:', response.data);
    return response.data;
  } catch (error) {
    console.error('❌ Error al obtener defectos críticos:', error);
    throw error;
  }
}

/**
 * Obtener estadísticas de defectos
 * 
 * @returns {Promise<Object>} Estadísticas agregadas de defectos
 * @throws {Error} Si hay un error en la petición HTTP
 */
export async function getDefectStatistics() {
  try {
    console.log('📊 Obteniendo estadísticas de defectos...');
    const response = await api.get('/defects/statistics');
    console.log('✅ Estadísticas de defectos obtenidas:', response.data);
    return response.data;
  } catch (error) {
    console.error('❌ Error al obtener estadísticas de defectos:', error);
    throw error;
  }
}

/**
 * Activar un defecto (cambiar estado a activo)
 * 
 * @param {number} id - ID del defecto a activar
 * @returns {Promise<Object>} Defecto activado
 * @throws {Error} Si hay un error en la petición HTTP
 */
export async function activateDefect(id) {
  try {
    console.log(`✅ Activando defecto ${id}`);
    const response = await api.patch(`/defects/${id}/activate`);
    console.log('✅ Defecto activado exitosamente');
    return response.data;
  } catch (error) {
    console.error(`❌ Error al activar defecto ${id}:`, error);
    throw error;
  }
}

/**
 * Desactivar un defecto (cambiar estado a inactivo)
 * 
 * @param {number} id - ID del defecto a desactivar
 * @returns {Promise<Object>} Defecto desactivado
 * @throws {Error} Si hay un error en la petición HTTP
 */
export async function deactivateDefect(id) {
  try {
    console.log(`❌ Desactivando defecto ${id}`);
    const response = await api.patch(`/defects/${id}/deactivate`);
    console.log('✅ Defecto desactivado exitosamente');
    return response.data;
  } catch (error) {
    console.error(`❌ Error al desactivar defecto ${id}:`, error);
    throw error;
  }
} 